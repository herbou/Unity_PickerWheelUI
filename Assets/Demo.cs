using UnityEngine ;
using EasyUI.PickerWheelUI ;
using UnityEngine.UI ;

public class Demo : MonoBehaviour {
   [SerializeField] private Button uiSpinButton ;
   [SerializeField] private Text uiSpinButtonText ;
   [SerializeField] private PickerWheel pickerWheel ;
   private void Start () {

      uiSpinButton.onClick.AddListener (() => {

         uiSpinButton.interactable = false ;
         uiSpinButtonText.text = "Spinning" ;

         pickerWheel.OnSpinEnd (wheelPiece => {
            Debug.Log (
               @" <b>Index:</b> " + wheelPiece.Index + "           <b>Label:</b> " + wheelPiece.Label
               + "\n <b>Point Rewarded</b> " + wheelPiece.value + "      <b>Chance:</b> " + wheelPiece.Chance + "%") ;

            uiSpinButton.interactable = true ;
            uiSpinButtonText.text = "Spin" ;
         }) ;
         Debug.Log("Total Cumulative Score:" + pickerWheel.score);
         pickerWheel.Spin () ;

      }) ;

   }
}
