using SlimCanvas.View.Controls.EventTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Collections
{
    internal class GlobalChildrenList
    {
        List<UIElement> _childs;

        //Increment
        int elementId;

        public GlobalChildrenList()
        {
            _childs = new List<UIElement>();
            elementId = 0;
        }

        public int GetNextElementId()
        {
            elementId++;
            return elementId;
        }

        public void Add(UIElement item)
        {
            _childs.Add(item);
        }

        public void AddRange(IEnumerable<UIElement> item)
        {
            _childs.AddRange(item);
        }

        public bool Remove(UIElement item)
        {
            try
            {
                if (item.Children.Count() > 0)
                {
                    var items = GetParentChild(item.Id);
                    foreach (var del in items)
                    {
                        Remove(del);
                        del.Dispose();
                        _childs.Remove(del);
                    }
                }

                item.Dispose();
                _childs.Remove(item);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<UIElement> GetParentChild(int parentId)
        {
            return _childs.Where(i => i.ParentId == parentId).ToList();
        }

        public void ClearParentChild(int parentId)
        {
            var liste = GetParentChild(parentId);
            foreach (var item in liste.ToList())
            {
                Remove(item);
            }
        }

        public IEnumerator<UIElement> GetEnumerator(int parentId)
        {
            return GetParentChild(parentId).GetEnumerator();
        }

        public List<UIElement> GetAll()
        {
            return _childs;
        }

        #region ElementsUnderPointXY

        public IEnumerable<BasicElement> ElementsUnderPointXY(PointerRoutedEventArgs e)
        {
            var elementsWithTrue = _childs.Where(v => v.PointerIsEntered == true).ToList();

            var elemensUnderPointer = _childs
                .Where(p => p.ActualX <= e.X && (p.ActualX + p.ActualWidth) >= e.X
                        && p.ActualY <= e.Y && (p.ActualY + p.ActualHeight) >= e.Y).OrderByDescending(i => i.Id).OrderBy(z => z.ZIndex).Cast<BasicElement>().ToList();

            foreach (var item in elemensUnderPointer)
            {
                var vorhanden = elementsWithTrue.FirstOrDefault(i => i.Id == item.Id);
                if (vorhanden != null)
                    elementsWithTrue.Remove(vorhanden);
                else
                {
                    item.PointerIsEntered = true;
                    item.PointerEnteredTrigger(e);
                }
            }

            foreach (var item in elementsWithTrue)
            {
                item.PointerExitedTrigger(e);
                item.PointerIsEntered = false;
            }

            elemensUnderPointer.Add(Canvas.MyCanvas);
            return elemensUnderPointer;
        }

        #endregion

        #region GetElementToDraw

        public IEnumerable<UIElement> GetElementToDraw()
        {
            var camX = Canvas.MyCanvas.Camera.X;
            var camY = Canvas.MyCanvas.Camera.Y;
            var camWidth = Canvas.MyCanvas.Camera.Width;
            var camHeight = Canvas.MyCanvas.Camera.Height;

            var liste = GetParentChild(Canvas.MyCanvas.Id).OrderByDescending(i => i.Id).OrderBy(z => z.ZIndex);

            var items = liste.Where(
                    p => (p.ActualX) < camX + camWidth &&
                    camX < (p.ActualX + p.ActualWidth) &&
                    (p.ActualY) < camY + camHeight &&
                    camY < (p.ActualY + p.ActualHeight)
                );

            return items;
        }

        #endregion
    }
}
