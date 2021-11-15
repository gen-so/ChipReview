using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Api
{
    /// <summary>
    /// Hard coded replys used for code
    /// </summary>
    public enum Reply
    {
        ValidationFailed,
        SendingToDNSFailed,
        UpdatingCacheFailed,
        DomainUpdated,
        DomainList,
        DomainNotAvailable,
        DomainAvailable,
        DomainAdded,
        AccountExists,
        NoAccountExists,
        AccountCreated,
        NotValidAccountData,
        InvalidAccountData,
        AccountNotCreated,
        AccountDeleted,
        UnexpectedError,
        DomainDeleted,
        ReviewList,
    }
}
