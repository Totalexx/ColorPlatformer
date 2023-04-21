using System.Collections.Generic;
using RougeBuilder.Model;

namespace RougeBuilder.System;

public interface ISystem
{
    void Update(IEnumerable<AbstractEntity> entities);
}