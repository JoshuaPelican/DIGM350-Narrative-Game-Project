using UnityEngine;

[CreateAssetMenu(fileName = "New Choice", menuName = "Dialogue/Choice")]
public class Choice : ScriptableObject
{
    public string Text;

    public bool Chosen;

    public Node NextNode;

    public Node Choose()
    {
        Chosen = true;
        return NextNode;
    }
}
