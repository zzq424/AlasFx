﻿using Microsoft.AspNetCore.Mvc;

using Rye.Web.Internal;

using System.Net;

namespace Rye.Web.ResponseProvider.AvoidRepeatableRequestAttr
{
    public class DefaultAvoidRepeatableRequestResponseProvider : IAvoidRepeatableRequestResponseProvider
    {
        public IActionResult CreateResponse(AvoidRepeatableRequestContext context)
        {
            return new JsonResult(Result
                .Create(HttpStatusCode.Forbidden, I18n.GetText(LangKeyEnum.AvoidRepeatableRequest)));
        }
    }
}
