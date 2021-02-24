using MyFinances.WebApi.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinances.WebApi.Models.Repositories
{
    public class OperationRepsoitory
    {
        private readonly MyFinancesContext _context;

        public OperationRepsoitory(MyFinancesContext context)
        {
            _context = context;
        }


        public IEnumerable<Operation> Get()
        {
            return _context.Operations;
        }

        public IEnumerable<Operation> Get(int recordsPerPage, int pageNr)
        {
            if (pageNr <= 0)
            {
                pageNr = 1;
            }
            int recordToSkip = recordsPerPage * (pageNr - 1);
            return _context.Operations.Skip(recordToSkip).Take(recordsPerPage);
        }

        // nie chcemy aby był rzucony wyjątek dlatego
        // nie dajemy ani Single() ani First()
        public Operation Get(int id)
        {
            return _context.Operations.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Operation operation)
        {
            operation.Date = DateTime.Now;
            _context.Operations.Add(operation);
        }

        public void Delete(int id)
        {
            var operationToDelete = _context.Operations.Single(x => x.Id == id);
            _context.Operations.Remove(operationToDelete);
        }

     
        public void Update(Operation operation)
        {
            var operationToUpdate = _context.Operations.Single(x => x.Id == operation.Id);
            operationToUpdate.Name = operation.Name;
            operationToUpdate.Value = operation.Value;
            operationToUpdate.CategoryId = operation.CategoryId;

        }

    }
}
