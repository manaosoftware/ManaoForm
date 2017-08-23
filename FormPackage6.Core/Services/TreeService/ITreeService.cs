using FormPackage6.Core.DomainModel.Tree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace FormPackage6.Core.Services.TreeService
{
    public interface ITreeService
    {
        List<NodeTreeModel> GetTrees();
        List<NodeTreeModel> GetTrees(Guid currentNodeId, bool allowOnlyRoot);
        List<NodeTreeModel> GetTreesByParentAndChildAliases(string[] parentAliases, string[] childAliases, Guid currentNodeId,bool allowOnlyRoot);
    }
}
