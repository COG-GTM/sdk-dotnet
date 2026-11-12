using System;

namespace Temporalio.Client.Interceptors
{
    /// <summary>
    /// Base class for all outbound interceptors.
    /// </summary>
    public abstract partial class ClientOutboundInterceptor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientOutboundInterceptor"/> class.
        /// </summary>
        /// <param name="next">Next outbound interceptor in the chain.</param>
        protected ClientOutboundInterceptor(ClientOutboundInterceptor next)
        {
            MaybeNext = next;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientOutboundInterceptor"/> class.
        /// </summary>
        private protected ClientOutboundInterceptor()
        {
        }

        /// <summary>
        /// Gets the next interceptor in the chain.
        /// </summary>
        protected ClientOutboundInterceptor Next =>
            MaybeNext ?? throw new InvalidOperationException("No next interceptor");

        /// <summary>
        /// Gets the namespace of the client this interceptor chain was created for.
        /// </summary>
        protected string Namespace => MaybeNext is { } next ? next.Namespace : RootNamespace;

        /// <summary>
        /// Gets the next interceptor in the chain if any.
        /// </summary>
        private protected ClientOutboundInterceptor? MaybeNext { get; init; }

        /// <summary>
        /// Gets the namespace for the interceptor at the root of the chain.
        /// </summary>
        private protected virtual string RootNamespace =>
            throw new InvalidOperationException("No next interceptor");
    }
}