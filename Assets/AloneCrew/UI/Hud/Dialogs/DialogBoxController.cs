using System.Collections;
using AloneCrew.Model.Data;
using PixelCrew.Utils;
using TMPro;
using UnityEngine;

namespace AloneCrew.UI.Hud.Dialogs
{
    public class DialogBoxController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private GameObject _container;
        [SerializeField] private Animator _animator;

        [Space] [SerializeField] private float _textSpeed = 0.09f;
        
        [Header("Sounds")] [SerializeField] private AudioClip _typingSound;
        [SerializeField] private AudioClip _open;
        [SerializeField] private AudioClip _close;

        private DialogData _data;
        private int _currentSentence;
        private AudioSource _sfxSource;
        private Coroutine _typingCoroutine;

        private void Start()
        {
            _sfxSource = AudioUtils.FindSfxSource();
        }
        
        public void ShowDialog(DialogData data)
        {
            _data = data;
            _currentSentence = 0;
            _text.text = string.Empty;
            
            _container.SetActive(true);
            _sfxSource.PlayOneShot(_open);
            _animator.SetBool("IsOpen", true);
        }


        public void OnSkip()
        {
            if (_typingCoroutine == null) return;
            StopTypeAnimation();
            _text.text = _data.Sentences[_currentSentence];
        }
        
        public void OnContinue()
        {
            StopTypeAnimation();
            _currentSentence++;
            var isDialogComplete = _currentSentence == _data.Sentences.Length;
            if (isDialogComplete)
            {
                HideDialogBox();
            }
            else
            {
                OnStartDialogAnimation();
            }
        }

        private void HideDialogBox()
        {
            _animator.SetBool("IsOpen", false);
            _sfxSource.PlayOneShot(_close);
            _container.SetActive(false);
        }
        private void StopTypeAnimation()
        {
            if (_typingCoroutine != null)
                StopCoroutine(_typingCoroutine);
            _typingCoroutine = null;
        }
        
        public void OnStartDialogAnimation()
        {
            _typingCoroutine = StartCoroutine(TypeDialogText());
        }
        
        public void OnCloseAnimationComplete()
        {
            
        }

        private IEnumerator TypeDialogText()
        {
           _text.text = string.Empty;
           var sentence = _data.Sentences[_currentSentence];
           foreach (var letter in sentence)
           {
               _text.text += letter;
               _sfxSource.PlayOneShot(_typingSound);
               yield return new WaitForSeconds(_textSpeed);
           }
           _typingCoroutine = null;
        }

        [SerializeField] private DialogData _testData;

        public void Test()
        {
            ShowDialog(_testData);
            
        }
        
    }
}