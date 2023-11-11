# АНАЛИЗ ДАННЫХ И ИСКУССТВЕННЫЙ ИНТЕЛЛЕКТ [in GameDev]
Отчет по лабораторной работе #4 выполнил:
- Лепинских Максим Игоревич
- РИ220943 

| Задание | Выполнение | Баллы |
| ------ | ------ | ------ |
| Задание 1 | * | 60 |
| Задание 2 | * | 20 |
| Задание 3 | * | 20 |

знак "*" - задание выполнено; знак "#" - задание не выполнено;

Работу проверили:
- к.т.н., доцент Денисов Д.В.
- к.э.н., доцент Панов М.А.
- ст. преп., Фадеев В.О.

[![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid)

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

## Цель работы
### Поработать со скриптом перцептрона, поэксперементировать с таблицами истинностями.

## Задание 1
### В проекте Unity реализовать перцептрон, который умеет производить вычисления:
#### OR | дать комментарии о корректности работы
#### AND | дать комментарии о корректности работы
#### NAND | дать комментарии о корректности работы
#### XOR | дать комментарии о корректности работы

#### Код перцептрона
```c#
public class Perceptron : MonoBehaviour
{
    public TrainingSet[] trainingSets;
    
    private readonly double[] _weights = { 0, 0 };
    private double _bias;
    private double _totalError;
    private int ErrorsNumber { get; set; }

    private double DotProductBias(IReadOnlyCollection<double> v1, IReadOnlyList<double> v2)
    {
        if (v1 == null || v2 == null || v1.Count != v2.Count) return -1;
        
        return v1.Select((t, x) => t * v2[x]).Sum() + _bias;
    }

    private double CalcOutput(int i) => DotProductBias(_weights, trainingSets[i].input) > 0 ? 1 : 0;

    private void InitialiseWeights()
    {
        for (var i = 0; i < _weights.Length; i++)
            _weights[i] = Random.Range(-1.0f, 1.0f);

        _bias = Random.Range(-1.0f, 1.0f);
    }

    private void UpdateWeights(int j)
    {
        var error = trainingSets[j].output - CalcOutput(j);
        _totalError += Mathf.Abs((float)error);

        for (var i = 0; i < _weights.Length; i++)
            _weights[i] += error * trainingSets[j].input[i];

        _bias += error;
    }

    private double CalcOutput(double i1, double i2)
    {
        var inp = new[] { i1, i2 };
        var dp = DotProductBias(_weights, inp);
        
        return dp > 0 ? 1 : 0;
    }

    private void Train(int epochsCnt)
    {
        InitialiseWeights();

        for (var e = 0; e < epochsCnt; e++)
        {
            _totalError = 0;
            for (var t = 0; t < trainingSets.Length; t++)
            {
                UpdateWeights(t);
                Debug.Log($"W1: {_weights[0]} W2: {_weights[1]} B: {_bias}");
            }
            Debug.Log($"Epoch: {e + 1} Errors: {_totalError}");
            
            if (_totalError > 0) ErrorsNumber++;
            else break;
        }
    }

    private void Start()
    {
        Train(20);
        Debug.Log("------------------------------------------------------------------");
        Debug.Log($"Result {trainingSets[0].input[0]} {trainingSets[0].input[1]}: " + CalcOutput(trainingSets[0].input[0], trainingSets[0].input[1]));
        Debug.Log($"Result {trainingSets[1].input[0]} {trainingSets[1].input[1]}: " + CalcOutput(trainingSets[1].input[0], trainingSets[1].input[1]));
        Debug.Log($"Result {trainingSets[2].input[0]} {trainingSets[2].input[1]}: " + CalcOutput(trainingSets[2].input[0], trainingSets[2].input[1]));
        Debug.Log($"Result {trainingSets[3].input[0]} {trainingSets[3].input[1]}: " + CalcOutput(trainingSets[3].input[0], trainingSets[3].input[1]));
    }

```

**OR**
![image](https://github.com/MAXBAF1/DA-in-GameDev-lab4/assets/63009846/8ddaebb8-0eb3-4c38-b1aa-64b8f49da7e4)
- Комментарий: перцептрон проявляет высокую скорость обучения, в среднем уже после 3-4 эпох он не демонстрирует ошибок.

**AND**
![image](https://github.com/MAXBAF1/DA-in-GameDev-lab4/assets/63009846/7c0a6cf0-845b-4c58-bc92-11339765833e)
- Комментарий: количество эпох обучения, при которых перцептрон не допускает ошибок, варьируется, отмечено, что минимальное значение составляет 3 эпохи, максимальное - 9, а в среднем этот процесс завершается после 6 эпох.

**NAND**
![image](https://github.com/MAXBAF1/DA-in-GameDev-lab4/assets/63009846/0d567b80-0fa0-4086-b4ea-0ca75143c2cc)
- Комментарий: Скорость обучения перцептрона снизилась, однако в среднем для достижения оптимальных результатов требуется 6-7 эпох.

**XOR**
![image](https://github.com/MAXBAF1/DA-in-GameDev-lab4/assets/63009846/5b03d850-090a-4006-83d0-e38b34faefdd)
- Комментарий: перцептрон не может обучится вне зависимости от количества эпох, допускает ошибки во всех 4-х случаях.

## 2 задание
### Построить графики зависимости количества эпох от ошибки обучения. Указать от чего зависит необходимое количество эпох обучения.

**OR**
![image](https://github.com/MAXBAF1/DA-in-GameDev-lab4/assets/63009846/a19961d1-c678-4870-a5e0-9626a9ec219d)
- Высокая скорость обучения (3-4 эпохи) может быть обусловлена тем, что операция OR имеет относительно простую структуру, что облегчает адаптацию персептрона к этой задаче.

**AND**
![image](https://github.com/MAXBAF1/DA-in-GameDev-lab4/assets/63009846/322b7d51-c3d0-4a01-877f-b4e105ed73b3)
- Вариация в количестве эпох (от 3 до 9, со средним значением 6) может указывать на то, что сложность операции AND требует более глубокого или тщательного обучения, и, вероятно, в зависимости от исходного состояния весовых коэффициентов.

**NAND**
![image](https://github.com/MAXBAF1/DA-in-GameDev-lab4/assets/63009846/42b1b8ea-2947-41db-ae71-79fa6a2425f8)
- Снижение скорости обучения (6-7 эпох) может свидетельствовать о том, что NAND, хотя и проще, чем XOR, может требовать некоторой дополнительной настройки весов для достижения оптимальных результатов.

**XOR**
![image](https://github.com/MAXBAF1/DA-in-GameDev-lab4/assets/63009846/305add07-adef-455d-91bd-7bf6503ec424)
Невозможность обучения персептрона вне зависимости от количества эпох и допущение ошибок во всех случаях может говорить о том, что XOR представляет собой задачу, которую персептрон не может решить в рамках текущей структуры. Вероятно, требуется более сложная архитектура с дополнительными слоями или другие методы обучения.

## Задание 3
### Построить визуальную модель работы перцептрона на сцене Unity.

Белый кубик - 1, темный - 0

- Для изменения цвета кубиков мне потребовалось создать ColorChanger:
```C#
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
```

**OR**

Изначально
![image](https://github.com/MAXBAF1/DA-in-GameDev-lab4/assets/63009846/a387c42f-3b5c-49f2-98a4-da96a4d7afe9)
В итоге
![image](https://github.com/MAXBAF1/DA-in-GameDev-lab4/assets/63009846/d6c50cb0-ad0c-4360-bc1f-26f61c87a48c)

**AND**

Изначально
![image](https://github.com/MAXBAF1/DA-in-GameDev-lab4/assets/63009846/fa16e0a9-f7ad-4e6b-998c-75d33d61b171)
В итоге
![image](https://github.com/MAXBAF1/DA-in-GameDev-lab4/assets/63009846/e64f077f-7f4b-40e3-8ade-887e7b7b1fb0)

**NAND**

Изначально
![image](https://github.com/MAXBAF1/DA-in-GameDev-lab4/assets/63009846/cc389bc5-78ee-4ecc-b56d-d9dea99b5e94)
В итоге
![image](https://github.com/MAXBAF1/DA-in-GameDev-lab4/assets/63009846/4d1ac47f-558e-4521-9f85-d2362dd4fe9c)

**XOR**

Изначально
![image](https://github.com/MAXBAF1/DA-in-GameDev-lab4/assets/63009846/7b4592a3-617c-4b8d-bc6f-d991e32f3628)
В итоге
![image](https://github.com/MAXBAF1/DA-in-GameDev-lab4/assets/63009846/e05a79a2-e452-4d69-83df-32b041f9fb59)

## Выводы
### В ходе лабораторной работы успешно реализованы и протестированы операции OR, AND, и NAND с использованием перцептрона в Unity. Проанализирована зависимость количества эпох от ошибки обучения, что позволяет лучше понять требования к обучению для разных операций. Визуальная модель в Unity добавляет практический аспект к полученным результатам.

| Plugin | README |
| ------ | ------ |
| Dropbox | [plugins/dropbox/README.md][PlDb] |
| GitHub | [plugins/github/README.md][PlGh] |
| Google Drive | [plugins/googledrive/README.md][PlGd] |
| OneDrive | [plugins/onedrive/README.md][PlOd] |
