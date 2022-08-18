using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Entity.Repositories
{
	public interface IGenericRepository<T> where T : class
	{
		Task<bool> Exists(Func<T, bool> expression = null);
	}
}
