using UnityEngine ;
using UnityEngine.UI ;
using DG.Tweening ;
using UnityEngine.Events ;

namespace EasyUI.PickerWheelUI {

   public class PickerWheel : MonoBehaviour {

      [Header ("References :")]
      [SerializeField] private GameObject linePrefab ;
      [SerializeField] private Transform linesParent ;

      [Space]
      [SerializeField] private Transform PickerWheelTransform ;
      [SerializeField] private Transform wheelCircle ;
      [SerializeField] private GameObject wheelPiecePrefab ;
      [SerializeField] private Transform wheelPiecesParent ;

      [Space]
      [Header ("Picker wheel settings :")]
      [SerializeField] [Range (1f, 15f)] private float spinDuration = 12f ;
      [SerializeField] [Range (.2f, 2f)] private float wheelSize = 1f ;

      [Space]
      [Header ("Picker wheel pieces :")]
      [SerializeField] private WheelPiece[] wheelPieces ;

      // Events
      private UnityAction onSpinStartEvent ;
      private UnityAction<WheelPiece> onSpinEndEvent ;


      private bool _isSpinning = false ;

      public bool IsSpinning { get { return _isSpinning ; } }


      private Vector2 pieceMinSize = new Vector2 (81f, 146f) ;
      private Vector2 pieceMaxSize = new Vector2 (144f, 213f) ;
      private int piecesMin = 2 ;
      private int piecesMax = 12 ;

      private float randomRotateAngle ;

      private float pieceAngle ;
      private float pieceHalfAngle ;

      private void Start () {
         pieceAngle = 360 / wheelPieces.Length ;
         pieceHalfAngle = pieceAngle / 2f ;

         randomRotateAngle = 600f * spinDuration ;

         Generate () ;  
      }

      private void Generate () {
         wheelPiecePrefab = InstantiatePiece () ;

         RectTransform rt = wheelPiecePrefab.transform.GetChild (0).GetComponent <RectTransform> () ;
         float pieceWidth = Mathf.Lerp (pieceMinSize.x, pieceMaxSize.x, 1f - Mathf.InverseLerp (piecesMin, piecesMax, wheelPieces.Length)) ;
         float pieceHeight = Mathf.Lerp (pieceMinSize.y, pieceMaxSize.y, 1f - Mathf.InverseLerp (piecesMin, piecesMax, wheelPieces.Length)) ;
         rt.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, pieceWidth) ;
         rt.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, pieceHeight) ;

         for (int i = 0; i < wheelPieces.Length; i++) {
            DrawPiece (i) ;
         }
         Destroy (wheelPiecePrefab) ;
      }

      private void DrawPiece (int index) {
         WheelPiece piece = wheelPieces [ index ] ;
         Transform pieceTrns = InstantiatePiece ().transform.GetChild (0) ;

         pieceTrns.GetChild (0).GetComponent <Image> ().sprite = piece.Icon ;
         pieceTrns.GetChild (1).GetComponent <Text> ().text = piece.Label ;
         pieceTrns.GetChild (2).GetComponent <Text> ().text = piece.Amount.ToString () ;

         //Line
         Transform lineTrns = Instantiate (linePrefab, linesParent.position, Quaternion.identity, linesParent).transform ;
         lineTrns.RotateAround (wheelPiecesParent.position, Vector3.back, (pieceAngle * index) + pieceHalfAngle) ;

         pieceTrns.RotateAround (wheelPiecesParent.position, Vector3.back, pieceAngle * index) ;
      }

      private GameObject InstantiatePiece () {
         return Instantiate (wheelPiecePrefab, wheelPiecesParent.position, Quaternion.identity, wheelPiecesParent) ;
      }

      public void Spin () {
         if (!_isSpinning) {
            _isSpinning = true ;
            if (onSpinStartEvent != null)
               onSpinStartEvent.Invoke () ;

            wheelCircle
            .DORotate (Vector3.back * Random.Range (randomRotateAngle, randomRotateAngle + 360f), spinDuration, RotateMode.Fast)
            .SetEase (Ease.InOutQuart)
            .OnComplete (() => {
               float angle = wheelCircle.eulerAngles.z + pieceHalfAngle ;
               int index = ((int)((angle * wheelPieces.Length) / 360f)) % wheelPieces.Length ;
               _isSpinning = false ;
               if (onSpinEndEvent != null)
                  onSpinEndEvent.Invoke (wheelPieces [ index ]) ;

               onSpinStartEvent = null ; 
               onSpinEndEvent = null ;
            }) ;
         }
      }

      public void OnSpinStart (UnityAction action) {
         onSpinStartEvent = action ;
      }

      public void OnSpinEnd (UnityAction<WheelPiece> action) {
         onSpinEndEvent = action ;
      }


      private void OnValidate () {
         if (PickerWheelTransform != null)
            PickerWheelTransform.localScale = new Vector3 (wheelSize, wheelSize, 1f) ;

         if (wheelPieces.Length > piecesMax || wheelPieces.Length < piecesMin)
            Debug.LogError ("[ PickerWheelwheel ]  pieces length must be between " + piecesMin + " and " + piecesMax) ;
      }
   }
}