using System;

namespace whostpos.Entitys.Interface
{
    public interface IEntity
    {
        Int64 Id { get; set; }
        bool IsDeleted { get; set; }
    }
}
