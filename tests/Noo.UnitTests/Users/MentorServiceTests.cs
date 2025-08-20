using Noo.Api.Users.Filters;
using Noo.Api.Users.Services;
using Noo.UnitTests.Common;

namespace Noo.UnitTests.Users;

public class MentorServiceTests
{

    [Fact]
    public async Task Assign_Unassign_And_Query_Assignments()
    {
        var dbName = Guid.NewGuid().ToString();
        using var context = TestHelpers.CreateInMemoryDb(dbName);
        var uow = TestHelpers.CreateUowMock(context).Object;
        var mentorService = new MentorService(uow);

        var studentId = Ulid.NewUlid();
        var mentorId = Ulid.NewUlid();
        var subjectId = Ulid.NewUlid();

        var assignmentId = await mentorService.AssignMentorAsync(studentId, mentorId, subjectId);
        Assert.NotEqual(default, assignmentId);

        // Assign again with same triple -> should not create duplicates
        var assignmentId2 = await mentorService.AssignMentorAsync(studentId, mentorId, subjectId);
        Assert.Equal(assignmentId, assignmentId2);

        var listForStudent = await mentorService.GetMentorAssignmentsAsync(studentId, new MentorAssignmentFilter { Page = 1, PerPage = 10 });
        Assert.Equal(1, listForStudent.Total);

        var listForMentor = await mentorService.GetStudentAssignmentsAsync(mentorId, new MentorAssignmentFilter { Page = 1, PerPage = 10 });
        Assert.Equal(1, listForMentor.Total);

        // Unassign in a fresh context to avoid tracking conflicts with DeleteById pattern
        using (var unassignCtx = TestHelpers.CreateInMemoryDb(dbName))
        {
            var unassignUow = TestHelpers.CreateUowMock(unassignCtx).Object;
            var unassignService = new MentorService(unassignUow);
            await unassignService.UnassignMentorAsync(assignmentId);
        }

        using (var verifyCtx = TestHelpers.CreateInMemoryDb(dbName))
        {
            var verifyUow = TestHelpers.CreateUowMock(verifyCtx).Object;
            var verifyService = new MentorService(verifyUow);
            var afterDelete = await verifyService.GetMentorAssignmentsAsync(studentId, new MentorAssignmentFilter { Page = 1, PerPage = 10 });
            Assert.Equal(0, afterDelete.Total);
        }
    }
}
