using System.Collections.Generic;

public interface IRoute<T> where T : Cell {
    IEnumerable<T> GetOptimalRoutes(T source);
    IEnumerable<T> GetAdditionalRoutes(T source);
}

