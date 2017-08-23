using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umbraco.cms.businesslogic.web;
using Umbraco.Core.Models;

namespace FormPackage6.Core.Services.UmbracoServices
{
    public interface IUmbracoService
    {
        IPublishedContent GetNodeById(int id);
        IPublishedContent GetContentByGuid(Guid id);
        IMedia GetMediaByGuid(Guid id);
        IContentType GetContentTypeById(int id);
        IPublishedContent GetHome(Guid contentId);
        IPublishedContent GetConfiguration(Guid currentContentId, string configAlias);
        string GetDictionaryItem(string dictionaryItem, string defaultValue);
        IDomain GetCurrentDomain();
    }
}
