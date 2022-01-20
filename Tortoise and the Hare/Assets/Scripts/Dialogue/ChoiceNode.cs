using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Choice Node", menuName = "Dialogue/Choice Node")]
public class ChoiceNode : Node
{
    public List<Choice> Choices;
}
