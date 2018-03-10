using System;
using Microsoft.EntityFrameworkCore;

namespace Entitys.EntityConfig
{
    public class EntityConfig : DbContext
    {
        public EntityConfig(DbContextOptions<EntityConfig> options)
            : base(options)
        {

        }
    }
}