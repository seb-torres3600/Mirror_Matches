using System;

// OBSERVER PATTERN
public interface Subject{
    public void addObserver(Observer o);
    public void removeObserver(Observer o);
    public void notifyObservers(bool redCond, Tuple<bool, double> result, Pawn attacker, Pawn target);
}