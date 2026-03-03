namespace Quantum
{
  public unsafe partial struct ShrinkingCircleState
  {
    public void EnterState(ShrinkingCircle* shrinkingCircle)
    {
      Log.Info($"Entering state: {CircleStateUnion.Field}");
      shrinkingCircle->CurrentTimeToNextState = TimeToNextState;
      switch (CircleStateUnion.Field)
      {
        case CircleStateUnion.INITIALSTATE:
          shrinkingCircle->TargetRadius = CircleStateUnion.InitialState->InitialRadius;
          break;
        case CircleStateUnion.PRESHRINKSTATE:
          shrinkingCircle->TargetRadius = CircleStateUnion.PreShrinkState->TargetRadius;
          break;
      }
    }
    
    public void Update(Frame f, ShrinkingCircle* shrinkingCircle)
    {
      if (shrinkingCircle->CurrentTimeToNextState <= 0)
        return;
      
      shrinkingCircle->CurrentTimeToNextState -= f.DeltaTime;
    }
  }
}