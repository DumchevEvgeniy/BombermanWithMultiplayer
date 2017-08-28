public interface IPonderable<T, outT> where T : class {
    outT GetWeight(T source, T next, T destinition);
}
