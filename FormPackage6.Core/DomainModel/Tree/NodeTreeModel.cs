using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormPackage6.Core.DomainModel.Tree
{
    public class NodeTreeModel
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public bool isAllowed { get; set; }
        public List<NodeTreeModel> childs { get; set; }

        public NodeTreeModel(int id, Guid guid, string name, string url, string icon, bool allowed)
        {
            this.Id = id;
            this.Guid = guid;
            this.Name = name;
            this.Url = url;
            this.Icon = icon;
            this.isAllowed = allowed;
            this.childs = new List<NodeTreeModel>();
        }
    }
}
