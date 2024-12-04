﻿using bumbo.Components;
using bumbo.Models;
using bumbo.ViewModels;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace bumbo.Controllers
{
    public class SwapShiftController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly ISwapShiftRequestRepository _swapShiftRequestRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IBranchHasEmployeeRepository _branchHasEmployeeRepository;

        public SwapShiftController(UserManager<Employee> userManager, ISwapShiftRequestRepository swapShiftRequestRepository, IScheduleRepository scheduleRepository, IEmployeeRepository employeeRepository, IBranchHasEmployeeRepository branchHasEmployeeRepository)
        {
            _userManager = userManager;
            _swapShiftRequestRepository = swapShiftRequestRepository;
            _scheduleRepository = scheduleRepository;
            _employeeRepository = employeeRepository;
            _branchHasEmployeeRepository = branchHasEmployeeRepository;
        }

        [Route("ShiftSwap")]
        public async Task<IActionResult> Index()
        {
            Employee user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            string employeeId = user.Id;

            List<SwitchRequest> incomingRequests = _swapShiftRequestRepository.GetAllIncomingRequests(employeeId);
            List<SwitchRequest> outgoingRequests = _swapShiftRequestRepository.GetAllOutgoingRequests(employeeId);

            SwapShiftViewModel viewModel = new SwapShiftViewModel
            {
                IncomingRequests = incomingRequests.Select(r => new SwitchRequestViewModel
                {
                    RequesterName = r.Employee?.FirstName + " " + r.Employee?.LastName,
                    ReceiverName = user.FirstName + " " + user.LastName,
                    Date = r.Date,
                    StartTime = r.Schedule.StartTime,
                    EndTime = r.Schedule.EndTime,
                    Department = r.Schedule.Department?.DepartmentName ?? "Onbekend",
                    Status = r.Declined ? "Geweigerd" : "In afwachting"
                }).ToList(),
                OutgoingRequests = outgoingRequests.Select(r => new SwitchRequestViewModel
                {
                    RequesterName = user.FirstName + " " + user.LastName,
                    ReceiverName = r.Employee?.FirstName + " " + r.Employee?.LastName,
                    Date = r.Date,
                    StartTime = r.Schedule.StartTime,
                    EndTime = r.Schedule.EndTime,
                    Department = r.Schedule.Department?.DepartmentName ?? "Onbekend",
                    Status = r.Declined ? "Geweigerd" : "Geaccepteerd"
                }).ToList()
            };

            return View(viewModel);
        }

        [Route("ShiftSwap/Create")]
        public async Task<IActionResult> Create()
        {
            SetTempDataForSwapShiftToast("CreateSwapShiftToast");

            // Haal de ingelogde gebruiker op
            Employee user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                TempData["ToastMessage"] = "Je moet ingelogd zijn om een dienstwissel aan te maken.";
                TempData["ToastType"] = "error";
                return RedirectToAction("AccessDenied", "Home");
            }

            try
            {
                string employeeId = user.Id;

                List<BranchHasEmployee> branches = _branchHasEmployeeRepository.GetBranchesForEmployee(employeeId);
                if (branches == null || !branches.Any())
                {
                    TempData["ToastMessage"] = "Geen filialen gekoppeld aan jouw account. Dienstwissel is niet mogelijk.";
                    TempData["ToastType"] = "error";
                    return RedirectToAction("Index");
                }

                int branchId = branches.First().BranchId;

                List<SwapShiftScheduleViewModel> schedules = _scheduleRepository.GetSchedulesForEmployee(employeeId)
                    .Select(s => new SwapShiftScheduleViewModel
                    {
                        Date = s.Date,
                        StartTime = s.StartTime,
                        EndTime = s.EndTime,
                        DepartmentName = s.DepartmentName
                    }).ToList();

                if (schedules == null || !schedules.Any())
                {
                    TempData["ToastMessage"] = "Geen diensten gevonden om te wisselen.";
                    TempData["ToastType"] = "error";
                    return RedirectToAction("Index");
                }

                CreateSwapShiftViewModel viewModel = new CreateSwapShiftViewModel
                {
                    EmployeeId = employeeId,
                    BranchId = branchId,
                    Schedules = schedules
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["ToastMessage"] = "Er is een fout opgetreden bij het laden van de dienstwissel: " + ex.Message;
                TempData["ToastType"] = "error";
                return RedirectToAction("Index");
            }
        }


        [HttpPost("ShiftSwap/ChooseEmployee")]
        public async Task<IActionResult> ChooseEmployee(string employeeId, int branchId, DateTime date)
        {
            SetTempDataForSwapShiftToast("ChooseEmployeeToast"); // Set temp data voor toasts

            Employee user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["ToastMessage"] = "Je moet ingelogd zijn om een medewerker te kiezen.";
                TempData["ToastType"] = "error";
                return RedirectToAction("AccessDenied", "Home");
            }

            try
            {
                
                Schedule schedule = _scheduleRepository.GetScheduleByEmployeeBranchDate(employeeId, branchId, DateOnly.FromDateTime(date));
                if (schedule == null)
                {
                    TempData["ToastMessage"] = "Schema niet gevonden voor de opgegeven datum en medewerker.";
                    TempData["ToastType"] = "error";
                    return RedirectToAction("Index");
                }

                List<Employee> availableEmployees = _employeeRepository.GetAvailableEmployees(
                    schedule.Date,
                    schedule.StartTime,
                    schedule.EndTime,
                    schedule.BranchId,
                    schedule.DepartmentName
                );

                if (availableEmployees == null || !availableEmployees.Any())
                {
                    TempData["ToastMessage"] = "Geen beschikbare medewerkers gevonden voor deze shift.";
                    TempData["ToastType"] = "error";
                    return RedirectToAction("Index");
                }

                ChooseEmployeeViewModel viewModel = new ChooseEmployeeViewModel
                {
                    ScheduleDate = schedule.Date,
                    StartTime = schedule.StartTime,
                    EndTime = schedule.EndTime,
                    DepartmentName = schedule.DepartmentName,
                    AvailableEmployees = availableEmployees.Select(e => new EmployeeViewModel
                    {
                        EmployeeId = e.Id,
                        Name = $"{e.FirstName} {e.LastName}"
                    }).ToList()
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["ToastMessage"] = "Er is een fout opgetreden bij het ophalen van de beschikbare medewerkers: " + ex.Message;
                TempData["ToastType"] = "error";
                return RedirectToAction("Index");
            }
        }


        [HttpPost("ShiftSwap/AssignEmployee")]
        public async Task<IActionResult> AssignEmployee(string EmployeeId, string ScheduleDate, string StartTime, string EndTime)
        {
            SetTempDataForSwapShiftToast("AssignEmployeeToast");

            try
            {
                Employee user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    TempData["ToastMessage"] = "Je moet ingelogd zijn met de gebruiker.";
                    TempData["ToastType"] = "error";
                    return RedirectToAction("AccessDenied", "Home");
                }

                if (!DateOnly.TryParse(ScheduleDate, out var date))
                {
                    TempData["ToastMessage"] = "Ongeldige datum. Controleer de invoer en probeer het opnieuw.";
                    TempData["ToastType"] = "error";
                    return RedirectToAction("Index");
                }
                if (!TimeOnly.TryParse(StartTime, out var startTime) || !TimeOnly.TryParse(EndTime, out var endTime))
                {
                    TempData["ToastMessage"] = "Ongeldige tijd. Controleer de invoer en probeer het opnieuw.";
                    TempData["ToastType"] = "error";
                    return RedirectToAction("Index");
                }

                var branch = _branchHasEmployeeRepository.GetBranchesForEmployee(user.Id).FirstOrDefault();
                if (branch == null)
                {
                    TempData["ToastMessage"] = "Je bent niet gekoppeld aan een filiaal. Deze functionaliteit is niet mogelijk.";
                    TempData["ToastType"] = "error";
                    return RedirectToAction("Index");
                }

                if (branch.BranchId == 0)
                {
                    TempData["ToastMessage"] = "Ongeldig filiaal. Neem contact op met de beheerder.";
                    TempData["ToastType"] = "error";
                    return RedirectToAction("Index");
                }

                var switchRequest = new SwitchRequest
                {
                    SendToEmployeeId = EmployeeId,
                    EmployeeId = user.Id,
                    BranchId = branch.BranchId,
                    Date = date,
                    Declined = false
                };

                _swapShiftRequestRepository.AddSwitchRequest(switchRequest);

                TempData["ToastMessage"] = "Wisselen van dienst aanvraag is verstuurd.";
                TempData["ToastType"] = "success";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ToastMessage"] = "Er is een fout opgetreden tijdens het aanmaken van de dienstwissel: " + ex.Message;
                TempData["ToastType"] = "error";
                return RedirectToAction("Index");
            }
        }


        private void SetTempDataForSwapShiftToast(string toastId)
        {
            TempData["ToastId"] = toastId;
            TempData["AutoHide"] = "yes";
            TempData["MilSecHide"] = 5000;
        }

    }
}
