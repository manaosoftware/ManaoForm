using FormPackage6.Core.Repositories;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormPackage6.Infrastructure.Mock
{
    public class MockData
    {
        public static void Mock(IContainer container)
        {
            if (!HasDbBeenCreatedNew(container))
            {
                //MockTestModel.CreateTestModel(container);
            }
        }

        private static bool HasDbBeenCreatedNew(IContainer container)
        {
            //var unitOfWork = container.GetInstance<IUnitOfWork>();
            //var test = unitOfWork.TestRepository.GetAll();

            //var tt = test.FirstOrDefault();
            //return tt != null;

            return false;
        }
    }
}
