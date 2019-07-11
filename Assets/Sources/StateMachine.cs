using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{

    public interface Decision{
        State decide(State currentState);
    }
    public abstract class State{
        public List<Decision> decisions {get; private set;} = new List<Decision>();  
        //public State decide(State currentState){}
        public virtual void enter(State from)
        {
        }
        public virtual void exit(State to)
        {
        }
        public virtual State update()
        {
            return null;
        }
    }

    State _currentState = null;

    public State currentState{

        get => _currentState;
        set{
            if(_currentState != value ){
                State oldState = _currentState;
                if(_currentState != null){
                    _currentState.exit(value);
                }
                
                _currentState = value;
                if(_currentState != null){
                    _currentState.enter(oldState);

                }
            }
        }
        
    }
    


    // Update is called once per frame
    public void update()
    {
        if(_currentState != null){
            State newState = _currentState.update();
            if(newState != null){
                currentState = newState;
            }
            else {
                foreach (Decision decision in currentState.decisions) {
                    if(decision != null) {
                        newState = decision.decide(currentState);
                        if (newState != null) {
                            currentState = newState;
                            break; 
                        }
                    }                   
                }
            }
        }
    }
}
