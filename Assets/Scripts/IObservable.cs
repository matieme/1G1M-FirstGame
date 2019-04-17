using UnityEngine;
using System.Collections;

public interface IObservable {

    void Subscribe(IObserver observer);
    void Unsubscribe(IObserver observer);
}