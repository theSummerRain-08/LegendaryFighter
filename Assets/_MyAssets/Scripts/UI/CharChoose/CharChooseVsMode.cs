using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameUltis;

public class CharChooseVsMode : MonoBehaviour {
    [SerializeField] Button nextButton;
    [SerializeField] Button undoButton;
    [SerializeField] CharChooseControl[] charChooseControls;

    [SerializeField] GameObject[] playerUi;
    [SerializeField] GameObject[] enemyUi;
    [SerializeField] GameObject enemyDefaultUi;
    protected void Awake() {
        nextButton.onClick.AddListener(OnclickNextButton);
        undoButton.onClick.AddListener(OnclickUndoButton);
    }
    private void Start() {
        SpecialUnityEvent.Instance.changePlayerChoose.AddListener(ChangeImg);
    }
    private void OnEnable() {
        CharChooseUI.SetPlayerChooseTurn();
        Hide(undoButton.gameObject);
        charChooseControls[(int)GameManager.Instance.playerChosen].ChangePlayerChosen();
        SetDefaultEnemyUI();
    }
    void OnclickNextButton() {
        if (CharChooseUI.isPlayerChooseTurn) {
            Show(undoButton.gameObject);
            CharChooseUI.isPlayerChooseTurn = false;
            PickRamdomEnemy();
        } else {
            // Test
            if (GameManager.Instance.gameMode != GameMode.Train) return;

            SceneManager.LoadScene(1);
        }
    }
    void OnclickUndoButton() {
        CharChooseUI.isPlayerChooseTurn = true;
        foreach (var control in charChooseControls) {
            control.Undo();
        }
        SetDefaultEnemyUI();
    }
    void ChangeImg(CharacterToChoose characterToChoose) {
        ShowObjInArray((int)characterToChoose, CharChooseUI.isPlayerChooseTurn ? playerUi : enemyUi);
        if (!CharChooseUI.isPlayerChooseTurn && enemyDefaultUi.activeSelf) 
            Hide(enemyDefaultUi);
    }
    void SetDefaultEnemyUI() {
        foreach (var ui in enemyUi) {
            Hide(ui);
        }
        Show(enemyDefaultUi);
    }
    void PickRamdomEnemy() {
        int ramdomValue = RandomValue(0, charChooseControls.Length - 1, (int)GameManager.Instance.playerChosen);
        charChooseControls[ramdomValue].ChangePlayerChosen();
    }
    int RandomValue(int startValue, int endValue, int notTargetValue) {
        int ramdomValue = RandomNumber(startValue, endValue);
        if (ramdomValue == notTargetValue) {
            ramdomValue = RandomValue(startValue, endValue, notTargetValue);
        }
        return ramdomValue;
    }
}