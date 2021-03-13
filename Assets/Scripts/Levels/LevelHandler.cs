using System.Collections;
using System.Collections.Generic;

public class LevelHandler
{
    private List<LevelInterface> levels;
    private int currentLevelNumber = 0;
    public LevelHandler()
    {

        this.levels = new List<LevelInterface>();
        this.levels.Add(new Oppgave1A());
    }

    public int getCurrentLevelNumber() {
        return this.currentLevelNumber;
    }

    public CannonState getCurrentLevelState(){
        return this.levels[currentLevelNumber].getInitialState();
    }

    public void goToNextLevel(){
        if (this.currentLevelNumber < this.levels.Count) {
            this.currentLevelNumber += 1;
        }
    }

    public void goToPreviousLevel(){
        if (this.currentLevelNumber > 0) {
            this.currentLevelNumber -= 1;
        }
    }
}
