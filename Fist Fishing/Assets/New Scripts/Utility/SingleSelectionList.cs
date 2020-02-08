using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SingleSelectionList<T> : ISingleSelectionList<T>
{
    protected List<T> m_Items = new List<T>();

    protected int m_selection;
    public int Selection => m_selection;
    public T SelectedItem => m_Items[Selection];

    public int Count => m_Items.Count;


    public void SetSelection(int selection)
    {
        if (m_Items.Count == 0)
        {
            m_selection = 0;
            return;
        }

        m_selection = (selection) % m_Items.Count;
        if (m_selection < 0)
            m_selection += m_Items.Count;
    }

    public void IncrementSelection(int direction)
    {
        SetSelection(m_selection + direction);
    }

    public void AddItem(T item)
    {
        m_Items.Add(item);
    }

    public void Remove(T item)
    {
        int removalIndex = m_Items.IndexOf(item);
        m_Items.RemoveAt(removalIndex);
        if (removalIndex <= m_selection)
            IncrementSelection(-1);
    }
}

