[System.Serializable]

public class InventoryItem 
{
    public InventoryItemDAta data;
    public int stackSize;
    public InventoryItem(InventoryItemDAta itemDAta){
        data = itemDAta;
        AddStack();
    }
    public void AddStack(){
        stackSize++;
    }
    public void RemoveFromStack(){
        stackSize--;
    }
}
