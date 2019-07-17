using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickup  {
    void ShowPickupDialog();
    void ClosePickupDialog();
    void PickupFailure();
    void PickupSuccess();
    void DropSuccess();
}
