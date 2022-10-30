
# Easy Fortune Spin Wheel UI
A powerful,Customizable, and esay-to-use Spin Wheel UI for Unity

### Video tutorial : https://youtu.be/jZ_cuxTPPsA
<br>

## ■ How to use?  :
### 1. Add ```DoTween``` package: http://dotween.demigiant.com/download.php
### 2. Add ```EasyUI_PickerWheel``` package.
### 3. Create a Canvas and add```PickerWheel``` prefab to it.
```Assets/PickerWheel/Prefabs/PickerWheel.prefab```
### 4. Create a ```Demo.cs``` script.
### 5. Add ```EasyUI.PickerWheelUI``` namespace.
### 6. Demo.cs :
```c#
using UnityEngine;
using EasyUI.PickerWheelUI;   //required

public class Demo : MonoBehaviour {
	// Reference to the PickerWheel GameObject (step 3):
	[SerializeField] private PickerWheel pickerWheel;
	
	private void Start () {
		// Start spinning:
		pickerWheel.Spin ();
	}
}
```

## ■ Wheel Events : ```OnSpinStart ```  and  ```OnSpinEnd ```  :
```c#
using UnityEngine;
using EasyUI.PickerWheelUI;

public class Demo : MonoBehaviour {
	[SerializeField] private PickerWheel pickerWheel;
	
	private void Start () {
		pickerWheel.OnSpinStart (() =>  {
			Debug.Log ("Spin start..."));
		});

		pickerWheel.OnSpinEnd (wheelPiece => {
			Debug.Log ("Spin end :") ;
			Debug.Log ("Index   : "+wheelPiece.Index);
			Debug.Log ("Chance  : "+wheelPiece.Chance);
			Debug.Log ("Label   : "+wheelPiece.Label);
			Debug.Log ("Amount  : "+wheelPiece.Amount);
		});

		pickerWheel.Spin ();
	}
}
```


<br><br>
<br>
## ❤️ Donate  
<a href="https://paypal.me/hamzaherbou" title="https://paypal.me/hamzaherbou" target="_blank"><img align="left" height="50" src="https://www.mediafire.com/convkey/72dc/iz78ys7vtfsl957zg.jpg" alt="Paypal"></a>

