using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Transform _mainCase;
    [SerializeField] private int _caseIndex;
    [SerializeField] private Material _whiteMaterial;
    [SerializeField] private Material _darkMaterial;

    private Transform _currentCase;

    private void Awake() => _currentCase = _mainCase.GetChild(_caseIndex);

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.GetComponent<ContactCube>()) return;
        
        var firstValue = int.Parse(_currentCase.GetChild(0).name);
        var secondValue = int.Parse(_currentCase.GetChild(1).name);

        if (_mainCase.GetComponent<OR>())
            ChangeColor(collision, firstValue + secondValue >= 1 ? _whiteMaterial.color : _darkMaterial.color);
        if (_mainCase.GetComponent<AND>())
            ChangeColor(collision, firstValue * secondValue == 1 ? _whiteMaterial.color : _darkMaterial.color);
        if (_mainCase.GetComponent<NAND>())
            ChangeColor(collision, firstValue * secondValue == 1 ? _darkMaterial.color : _whiteMaterial.color);
        if (_mainCase.GetComponent<XOR>())
            ChangeColor(collision, firstValue == secondValue ? _darkMaterial.color : _whiteMaterial.color);
    }

    private void ChangeColor(Collision collision, Color color)
    {
        collision.gameObject.GetComponent<MeshRenderer>().materials[0].color = color;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}