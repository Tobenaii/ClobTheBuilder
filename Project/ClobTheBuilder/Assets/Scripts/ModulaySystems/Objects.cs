using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


//<summary>
//Used for storing reference variables to unique objects of type that inherits from class T
//Variables that use the same type as another will be ignored
//</summary>
public abstract class Objects<T> : ScriptableObject where T : class
{
    //<summary>
    //Returns the first public variable that is of type P
    //</summary>
    public T GetObject<P>() where P : T
    {
        var fields = this.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        foreach (var field in fields)
        {
            if (field.GetValue(this) is P obj)
                return obj;
        }
        return null;
    }
}
