using System;
using DG.Tweening;
using Quantum;
using UnityEngine;

namespace QuantumUser.View
{
  public class ShrinkingCircleView : QuantumEntityViewComponent
  {
    [SerializeField] private Transform redCircle;
    [SerializeField] private Transform whiteCircle;

    public override void OnActivate(Frame frame)
    {
      QuantumEvent.Subscribe<EventShrinkingCircleStateChanged>(this, OnShrinkingCircleStateChanged);

      var shrinkingCircle = frame.GetSingleton<ShrinkingCircle>();
      whiteCircle.localScale = redCircle.localScale =
        new Vector3(shrinkingCircle.CurrentRadius.AsFloat, shrinkingCircle.CurrentRadius.AsFloat);
      redCircle.gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
      var shrinkingCircle = VerifiedFrame.GetSingleton<ShrinkingCircle>();
      if (shrinkingCircle.CurrentState.CircleStateUnion.Field != CircleStateUnion.SHRINKSTATE)
        return;
      redCircle.localScale = new Vector3(shrinkingCircle.CurrentRadius.AsFloat, shrinkingCircle.CurrentRadius.AsFloat);
    }

    private void OnShrinkingCircleStateChanged(EventShrinkingCircleStateChanged e)
    {
      var shrinkingCircle = VerifiedFrame.GetSingleton<ShrinkingCircle>();
      var currentState = shrinkingCircle.CurrentState.CircleStateUnion.Field;
      redCircle.gameObject.SetActive(currentState is CircleStateUnion.PRESHRINKSTATE or CircleStateUnion.SHRINKSTATE);
      if (currentState is CircleStateUnion.PRESHRINKSTATE)
        whiteCircle.DOScale(new Vector3(shrinkingCircle.TargetRadius.AsFloat, shrinkingCircle.TargetRadius.AsFloat), 1f);
    }

    public override void OnDeactivate()
    {
      QuantumEvent.UnsubscribeListener(this);
    }
  }
}