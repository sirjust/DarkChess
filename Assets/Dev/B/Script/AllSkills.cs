using UnityEngine;

public enum Skills
{
    Strike
}

public class AllSkills : MonoBehaviour
{
    public TurnSystem turnSystem;
    private EditedGridGenerator gridGenerator;
    private int targets = 0;

    public bool cast(Card card, EditedGridGenerator _gridGenerator, GameObject _user)
    {
        gridGenerator = _gridGenerator;

        if(turnSystem.GetBattleStatus() != BattleStatus.PlayerCombat)
        {
            Debug.Log("Its not your turn");
            return false;
        }

        foreach (GameObject tile in gridGenerator.selectedTiles)
        {
            foreach (GameObject tile1 in gridGenerator.skillrangeTiles)
            {
                if (tile.transform.position.x == tile1.transform.position.x && tile.transform.position.z == tile1.transform.position.z)
                {
                    this.SendMessage(card.skill.ToString(), tile);
                    targets++;

                    if (targets >= card.maxAmountOfTargets) return true;
                }
            }
        }
        if (targets == 0)
        {
            Debug.LogError("Select other tiles");
            return false;
        }

        return true;
    }

    public void Strike(GameObject targetTile)
    {
        turnSystem.NextTurn();
        Debug.Log($"strike at {targetTile.GetComponent<GetObjectonTile>().gameObjectOnTile.name}");
    }
}
