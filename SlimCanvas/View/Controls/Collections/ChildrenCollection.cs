using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Collections
{
    /// <summary>
    /// ChildrenCollection
    /// </summary>
    public class ChildrenCollection : IEnumerable<UIElement>
    {
        int parentId;
        BasicElement parent;
        
        internal ChildrenCollection(int id, BasicElement element)
        {
            parent = element;
            parentId = id;
        }

        /// <summary>
        /// Add item
        /// </summary>
        /// <param name="item"></param>
        public void Add(UIElement item)
        {
            Canvas.GlobalChildrenList.Add(item);

            item.ParentId = parentId;
            item.Parent = parent;
        }

        /// <summary>
        /// Add range
        /// </summary>
        /// <param name="items"></param>
        public void AddRange(IEnumerable<UIElement> items)
        {
            Canvas.GlobalChildrenList.AddRange(items);

            foreach (var item in items)
            {
                item.ParentId = parentId;
                item.Parent = parent;
            }
        }

        /// <summary>
        /// Clear all children
        /// </summary>
        public void Clear()
        {
            Canvas.GlobalChildrenList.ClearParentChild(parentId);
        }

        /// <summary>
        /// Remove any children
        /// </summary>
        /// <param name="item"></param>
        public void Remove(UIElement item)
        {
            Canvas.GlobalChildrenList.Remove(item);
        }

        /// <summary>
        /// to be added
        /// </summary>
        /// <returns></returns>
        public IEnumerator<UIElement> GetEnumerator()
        {
            return Canvas.GlobalChildrenList.GetEnumerator(parentId);
        }

        /// <summary>
        /// to be added
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
