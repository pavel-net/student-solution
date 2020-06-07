using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Student.TestProject
{
    public class AsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public AsyncEnumerable(Expression expression)
            : base(expression) { }

        public IAsyncEnumerator<T> GetEnumerator() =>
            new AsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
        {
            //return new TestDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
            return GetEnumerator();
        }
    }

    public class AsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> enumerator;

        public AsyncEnumerator(IEnumerator<T> enumerator) =>
            this.enumerator = enumerator ?? throw new ArgumentNullException();

        //public IAsyncEnumerator<T> GetEnumerator() =>
        //    new AsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());

        public ValueTask<bool> MoveNextAsync()
        {
            return new ValueTask<bool>(Task.FromResult(enumerator.MoveNext()));
        }

        public T Current => enumerator.Current;

        public void Dispose() { }

        public Task<bool> MoveNext(CancellationToken cancellationToken) =>
            Task.FromResult(enumerator.MoveNext());

        public ValueTask DisposeAsync()
        {
            return new ValueTask();
        }
    }
}
