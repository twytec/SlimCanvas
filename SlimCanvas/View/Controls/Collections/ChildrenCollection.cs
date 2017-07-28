using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Collections
{
    public class ChildrenCollection : IEnumerable<UIElement>
    {
        int parentId;
        BasicElement parent;
        public ChildrenCollection(int id, BasicElement element)
        {
            parent = element;
            parentId = id;
        }

        public void Add(UIElement item)
        {
            Canvas.GlobalChildrenList.Add(item);

            item.ParentId = parentId;
            item.Parent = parent;
        }

        public void AddRange(IEnumerable<UIElement> items)
        {
            Canvas.GlobalChildrenList.AddRange(items);

            foreach (var item in items)
            {
                item.ParentId = parentId;
                item.Parent = parent;
            }
        }

        public void Clear()
        {
            Canvas.GlobalChildrenList.ClearParentChild(parentId);
        }

        public void Remove(UIElement item)
        {
            Canvas.GlobalChildrenList.Remove(item);
        }

        public IEnumerator<UIElement> GetEnumerator()
        {
            return Canvas.GlobalChildrenList.GetEnumerator(parentId);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
