using Microsoft.AspNetCore.Mvc;
using Csis.Admission.Domain.Entities;
using Csis.Admission.Application.Common.Interfaces;

namespace Csis.Admission.WebApi.Controllers;

/// <summary>
/// Test
/// </summary>
[Route("test")]
public class TestController : ApiControllerBase
{
    private readonly IRepository<Person> _pRepo;
    private readonly IRepository<Student> _sRepo;

    //private readonly AppDbContext _dbContext;

    /// <summary>
    /// Test
    /// </summary>
    /// <param name="pRepo"></param>
    /// <param name="sRepo"></param>
    public TestController(IRepository<Person> pRepo, IRepository<Student> sRepo) {
        _pRepo = pRepo;
        _sRepo = sRepo;
        //_dbContext = dbContext;
    }

    /// <summary>
    /// Test
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    //[CsisAuthorize]
    public async Task<IActionResult> Test() {
        var p = new Person {
            //Id = 1,
            FirstName = "Mark",
            LastName = "Johnson",
            Mobile = "09120000000"
        };

        // var p = await _dbContext.Set<Person>().AsTracking().FirstAsync(x=>x.Id == 2);
        await _pRepo.InsertAsync([p]);

        p.FirstName = "John Updated";
        p.LastName = "Doe Updated";
        p.Mobile = "09120000000";

        //p.FirstName = "John Updated";
        //p.LastName = "Doe Updated";
        //p.Mobile = "09120000001";
        await _pRepo.UpdateAsync([p]);

        //p.SoftDelete();
        //_dbContext.Update(p);
        await _pRepo.DeleteAsync(p, autoSave: false);
        await _pRepo.SaveAsync();

        return Ok(p);
    }

    /*[HttpPost("student")]
    public async Task<IActionResult> Std() {
        //var s = new Student {
        //    Branch = 1
        //};

        //await _dbContext.AddAsync(s);
        //await _dbContext.SaveChangesAsync();

        var s =await _dbContext.Set<Student>().FindAsync([1]);

        //s.CaseBlockReasons.Remove(Domain.Enums.CaseBlockReason.Cancellation);
        //s.CaseBlockReasons.Add(Domain.Enums.CaseBlockReason.InactiveFile);
        s.SetCaseBlockReasons(new List<CaseBlockReason> {
            CaseBlockReason.Cancellation,
            CaseBlockReason.Graduation,
            CaseBlockReason.SpecialCourt
        });
        s.Agency = 1;
        s.CaseCreateDate = 14040102;
        _dbContext.Update(s);
        await _dbContext.SaveChangesAsync();

        return Ok(s);
    }*/

    /// <summary>
    /// Repo
    /// </summary>
    /// <returns></returns>
    [HttpPost("repo")]
    public async Task<IActionResult> Repo() {
        var p = new Person {
            FirstName = "Markss",
            LastName = "Johnson",
            Mobile = "09120000000"
        };
        var p2 = new Person {
            FirstName = "Mark3",
            LastName = "Johnson",
            Mobile = "09120000000"
        };
        var p3 = new Person {
            FirstName = "Mark23s",
            LastName = "Johnson",
            Mobile = "09120000000"
        };

        //var s = new Student {
        //    Agency = 1,
        //};

        var s2 = await _pRepo.GetByIdAsTrackingAsync(28);
        s2.FirstName = "Updated2";
        s2.FatherName = "Updated";

        await _pRepo.UpdateAsync(s2, autoSave: false);
        //await _sRepo.InsertAsync(s, autoSave: false);
        await _sRepo.BulkSaveAsync();
        // _pRepo.BulkInsertAsync([p,p2,p3]);
        //await _pRepo.BulkSaveAsync();
        //await _sRepo.InsertAsync(s);

        return Ok();
    }
}
