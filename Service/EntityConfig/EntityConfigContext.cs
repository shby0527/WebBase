using System;
using Entitys.Interface;
using Microsoft.EntityFrameworkCore;

namespace Service.EntityConfig
{
    public class EntityConfigContext : DbContext, IDbPool
    {
        public EntityConfigContext(DbContextOptions<EntityConfigContext> options)
            : base(options)
        {

        }
    }
}