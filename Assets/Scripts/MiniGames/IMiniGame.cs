using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Callback(bool won);

public interface IMiniGame {
    void StartGame(Callback cb);
}
