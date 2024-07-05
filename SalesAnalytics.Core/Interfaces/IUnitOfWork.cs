using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAnalytics.Core
{
	public interface IUnitOfWork
	{
		Task CompleteAsync();
	}
}
