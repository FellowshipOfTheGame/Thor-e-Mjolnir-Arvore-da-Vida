﻿using UnityEngine;

namespace ThorGame.Trees
{
    public interface INode
    {
        bool IsRoot { get; }
        
        ConnectionCount InputConnection { get; }
        ConnectionCount OutputConnection { get; }
        
        string Title { get; }
        
#if UNITY_EDITOR
        Vector2 TreePos { get; set; }
        string TreeGuid { get; set; }
#endif
    }
}