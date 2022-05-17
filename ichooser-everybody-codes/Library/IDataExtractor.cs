namespace Library
{
    public interface IDataExtractor
    {
        /// <summary>
        /// Gets the data as an <seealso cref="IList{T}"/>
        /// </summary>
        /// <typeparam name="T">The type of data.</typeparam>
        /// <returns></returns>
        IList<T> GetData<T>();
    }
}