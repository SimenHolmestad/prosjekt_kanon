using System.Collections;
using System.Collections.Generic;

public class LevelHandler
{
    private List<LevelInterface> levels;
    private int currentLevelNumber = 0;
    public LevelHandler()
    {

        this.levels = new List<LevelInterface>();
        this.levels.Add(new Oppgave0());
        this.levels.Add(new Oppgave1A());
        this.levels.Add(new Oppgave1B());
        this.levels.Add(new Oppgave1C());
        this.levels.Add(new Oppgave2A());
        this.levels.Add(new Oppgave2B());
        this.levels.Add(new Oppgave2C());
        this.levels.Add(new Oppgave3A());
        this.levels.Add(new Oppgave3B());
    }

    public int getCurrentLevelNumber() {
        return this.currentLevelNumber;
    }

    public CannonState getCurrentLevelState(){
        return this.levels[currentLevelNumber].getInitialState();
    }

    public void goToNextLevel(){
        if (this.currentLevelNumber < this.levels.Count - 1) {
            this.currentLevelNumber += 1;
        }
    }

    public void goToPreviousLevel(){
        if (this.currentLevelNumber > 0) {
            this.currentLevelNumber -= 1;
        }
    }
}
