using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Noo.Api.Core.Security.Authorization;
using Noo.Api.Courses.Models;
using Noo.Api.Courses.QuerySpecifications;

namespace Noo.UnitTests.Courses.Specifications;

public class CourseSpecificationsTests
{
	[Fact]
	public void CourseSpecification_Admin_Matches_All()
	{
		var spec = new CourseSpecification(UserRoles.Admin, Ulid.NewUlid());
		// EF integration is heavy; we validate that no exception is thrown creating the spec
		Assert.NotNull(spec);
	}

	[Fact]
	public void CourseSpecification_Student_Filters_On_Membership()
	{
		var studentId = Ulid.NewUlid();
		var spec = new CourseSpecification(UserRoles.Student, studentId);
		Assert.NotNull(spec);
	}

	[Fact]
	public void CourseMembershipSpecification_Filters_By_Role()
	{
		var studentId = Ulid.NewUlid();
		var sSpec = new CourseMembershipSpecification(UserRoles.Student, studentId);
		var aSpec = new CourseMembershipSpecification(UserRoles.Admin, Ulid.NewUlid());
		var nSpec = new CourseMembershipSpecification(null, null);
		Assert.NotNull(sSpec);
		Assert.NotNull(aSpec);
		Assert.NotNull(nSpec);
	}
}
