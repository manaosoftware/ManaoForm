using FormPackage6.Core.DomainModel.Tree;
using FormPackage6.Core.Services.TreeService;
using System.Collections;
using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Web;
using System.Linq;
using Umbraco.Core.Services;
using System;

namespace FormPackage6.Infrastructure.UmbracoServices
{

    public class TreeService : ITreeService
    {
        private UmbracoHelper umbracoHelper;
        private IContentTypeService contentTypeService;
        List<NodeTreeModel> nodes;
        public TreeService(UmbracoHelper umbracoHelper, IContentTypeService contentTypeService)
        {
            this.umbracoHelper = umbracoHelper;
            this.contentTypeService = contentTypeService;
            this.nodes = new List<NodeTreeModel>();
        }
        public List<NodeTreeModel> GetTrees()
        {
            return recursiveTrees(umbracoHelper.TypedContentAtRoot());
        }
        public List<NodeTreeModel> GetTrees(Guid currentNodeId, bool allowOnlyRoot)
        {
            List<NodeTreeModel> nodeTrees = new List<NodeTreeModel>();
            if (allowOnlyRoot)
            {
                var content = umbracoHelper.TypedContent(currentNodeId);
                if (content != null)
                {
                    var rootNode = umbracoHelper.TypedContent(currentNodeId).AncestorOrSelf(1);
                    nodeTrees = recursiveTrees(new List<IPublishedContent>() { rootNode });
                }
            }
            else
            {
                nodeTrees = recursiveTrees(umbracoHelper.TypedContentAtRoot());
            }
            return nodeTrees;
        }
        public List<NodeTreeModel> GetTreesByParentAndChildAliases(string[] parentAliases, string[] childAliases, Guid currentNodeId, bool allowOnlyRoot)
        {
            if (allowOnlyRoot)
            {
                var content = umbracoHelper.TypedContent(currentNodeId);
                if (content != null)
                {
                    var rootNode = umbracoHelper.TypedContent(currentNodeId).AncestorOrSelf(1);
                    recursiveParentSearch(new List<IPublishedContent>() { rootNode }, parentAliases, childAliases);
                }
            }
            else
            {
                recursiveParentSearch(umbracoHelper.TypedContentAtRoot(), parentAliases, childAliases);
            }

            return nodes;
        }
        private List<NodeTreeModel> recursiveTrees(IEnumerable<IPublishedContent> nodes)
        {
            List<NodeTreeModel> nodeList = new List<NodeTreeModel>();
            foreach (IPublishedContent child in nodes)
            {
                var documentType = contentTypeService.GetContentType(child.DocumentTypeId);
                NodeTreeModel nodeModel = new NodeTreeModel(child.Id, child.GetKey(), child.Name, child.Url, documentType.Icon, true);
                nodeList.Add(nodeModel);
                if (child.Children.Count() > 0)
                {
                    nodeModel.childs = recursiveTrees(child.Children);
                }
            }
            return nodeList;
        }
        private void recursiveParentSearch(IEnumerable<IPublishedContent> parents, string[] parentAliases, string[] childAliases)
        {
            foreach (IPublishedContent node in parents)
            {
                foreach (var alias in parentAliases)
                {
                    if (node.ContentType.Alias.Equals(alias))
                    {
                        if (IsNodeTreesValid(node.GetKey()))
                        {
                            var documentType = contentTypeService.GetContentType(node.DocumentTypeId);
                            NodeTreeModel nodeModel = new NodeTreeModel(node.Id, node.GetKey(), node.Name, node.Url, documentType.Icon, false);
                            nodeModel.childs = recursiveChildSearch(node, parentAliases, childAliases);
                            if (childAliases.Contains(node.DocumentTypeAlias))
                            {
                                nodeModel.isAllowed = true;
                            }
                            nodes.Add(nodeModel);
                        }
                    }
                }
                if (node.Children.Count() > 0)
                {
                    recursiveParentSearch(node.Children, parentAliases, childAliases);
                }
            }
        }

        //This method for avoid duplicate Node in list
        private bool IsNodeTreesValid(Guid nodeId)
        {
            bool result = true;

            var stack = new Stack<NodeTreeModel>(nodes); 
            while (stack.Count > 0)
            {
                var next = stack.Pop();

                if(next.Guid == nodeId)
                {
                    result = false;
                    break;
                }
                foreach (var child in next.childs)
                {
                    stack.Push(child);
                } 
            }

            return result;
        }

        private List<NodeTreeModel> recursiveChildSearch(IPublishedContent parent, string[] parentAliases, string[] childAliases)
        {
            List<NodeTreeModel> List = new List<NodeTreeModel>();
            foreach (var child in parent.Children)
            {
                var documentType = contentTypeService.GetContentType(child.DocumentTypeId);
                bool isParent = false;
                NodeTreeModel nodeModel = new NodeTreeModel(child.Id, child.GetKey(), child.Name, child.Url, documentType.Icon, false);
                foreach (var alias in parentAliases)
                {
                    if (child.ContentType.Alias.Equals(alias))
                    {
                        isParent = true;
                    }
                }
                foreach (var alias in childAliases)
                {
                    if (child.ContentType.Alias.Equals(alias))
                    {
                        nodeModel.isAllowed = true;
                    }
                }
                if (child.Children.Count() > 0)
                {
                    nodeModel.childs = recursiveChildSearch(child, parentAliases, childAliases);
                }
                if (nodeModel.isAllowed || (nodeModel.childs.Count > 0 && !isParent))
                {
                    List.Add(nodeModel);
                }
            }
            return List;
        }
    }
}
