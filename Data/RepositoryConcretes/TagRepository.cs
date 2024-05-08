using Core.Models;
using Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.RepositoryConcretes
{
    public class TagRepository: GenericRepository<Tag>
    {
        public TagRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
