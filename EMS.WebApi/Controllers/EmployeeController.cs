using EMS.Domain;
using EMS.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMS.WebApi.Controllers;

public class EmployeeController(ApplicationDbContext context) : Controller
{
  // GET: api/Employee
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
    {
        return await context.Employees.Include(e => e.PersonalDetails).ToListAsync();
    }

    // GET: api/Employee/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetEmployee(int id)
    {
        var employee = await context.Employees.Include(e => e.PersonalDetails)
                                               .FirstOrDefaultAsync(e => e.EmployeeId == id);

        if (employee == null)
        {
            return NotFound();
        }

        return employee;
    }

    // POST: api/Employee
    [HttpPost]
    public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
    {
        context.Employees.Add(employee);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeId }, employee);
    }

    // PUT: api/Employee/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEmployee(int id, Employee employee)
    {
        if (id != employee.EmployeeId)
        {
            return BadRequest();
        }

        context.Entry(employee).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EmployeeExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // PUT: api/Employee/5/PersonalDetails
    [HttpPut("{id}/PersonalDetails")]
    public async Task<IActionResult> PutPersonalDetails(int id, PersonalDetails personalDetails)
    {
        var employee = await context.Employees.Include(e => e.PersonalDetails)
                                               .FirstOrDefaultAsync(e => e.EmployeeId == id);

        if (employee == null)
        {
            return NotFound();
        }

        if (employee.PersonalDetails == null)
        {
            personalDetails.EmployeeId = id;
            context.PersonalDetails.Add(personalDetails);
        }
        else
        {
            employee.PersonalDetails.Address = personalDetails.Address;
            employee.PersonalDetails.PhoneNumber = personalDetails.PhoneNumber;
            employee.PersonalDetails.Email = personalDetails.Email;
            context.Entry(employee.PersonalDetails).State = EntityState.Modified;
        }

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EmployeeExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Employee/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var employee = await context.Employees.Include(e => e.PersonalDetails)
                                               .FirstOrDefaultAsync(e => e.EmployeeId == id);
        if (employee == null)
        {
            return NotFound();
        }

        context.Employees.Remove(employee);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool EmployeeExists(int id)
    {
        return context.Employees.Any(e => e.EmployeeId == id);
    }
}
