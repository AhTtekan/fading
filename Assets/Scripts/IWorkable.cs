using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWorkable {
    
    void Work();

    void SetWorkable(bool input);
    void ShowWorkDialog();
    void HideWorkDialog();
}
