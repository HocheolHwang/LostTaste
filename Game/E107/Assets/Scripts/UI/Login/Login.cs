using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Login : MonoBehaviour
{
    // 로그인 입력 필드
    public TMP_InputField loginInputID; // 로그인 아이디 입력 필드
    public TMP_InputField loginInputPW; // 로그인 비밀번호 입력 필드

    // 회원가입 입력 필드
    public TMP_InputField signUpInputID; // 회원가입 아이디 입력 필드
    public TMP_InputField signUpInputNickname; // 회원가입 닉네임 입력 필드
    public TMP_InputField signUpInputPW; // 회원가입 비밀번호 입력 필드
    public TMP_InputField signUpInputPWConfirm; // 회원가입 비밀번호 확인 입력 필드

    // 버튼
    public Button loginButton; // 로그인 버튼
    public Button signUpButton; // 회원가입 버튼
    public Button showLoginButton; // 로그인 창 표시 버튼
    public Button showSignUpButton; // 회원가입 창 표시 버튼

    // 패널
    public GameObject loginPanel; // 로그인 패널
    public GameObject signUpPanel; // 회원가입 패널

    // 사용자가 로그인을 시도할 때 발생하는 이벤트
    public event Action<string, string> onClickLogin;

    // 스크립트가 활성화되었을 때 호출
    private void Awake()
    {
        // 로그인 버튼에 클릭 이벤트를 추가
        if (loginButton != null)
        {
            this.loginButton.onClick.AddListener(HandleLoginButtonClick);
        }

        // 회원가입 버튼에 클릭 이벤트를 추가
        if (signUpButton != null)
        {
            this.signUpButton.onClick.AddListener(HandleSignUpButtonClick);
        }

        // 로그인 창 표시 버튼에 클릭 이벤트를 추가합니다.
        if (showLoginButton != null)
        {
            this.showLoginButton.onClick.AddListener(ShowLoginPanel);
        }

        // 회원가입 창 표시 버튼에 클릭 이벤트를 추가합니다.
        if (showSignUpButton != null)
        {
            this.showSignUpButton.onClick.AddListener(ShowSignupPanel);
        }
    }

    // 로그인 버튼 클릭 시 호출되는 함수
    private void HandleLoginButtonClick()
    {
        // 입력된 아이디와 비밀번호를 가져옴
        string id = loginInputID.text;
        string pw = loginInputPW.text;

        // 아이디와 비밀번호를 디버그 로그로 출력
        Debug.LogFormat("로그인 정보: id: {0}, pw={1}", id, pw);

        // onClickLogin 이벤트를 호출하여 로그인을 시도
        onClickLogin?.Invoke(id, pw);
    }

    // 회원가입 버튼 클릭 시 호출되는 함수
    private void HandleSignUpButtonClick()
    {
        // 입력된 아이디, 닉네임, 비밀번호, 비밀번호 확인을 가져옴
        string id = signUpInputID.text;
        string nickname = signUpInputNickname.text;
        string pw = signUpInputPW.text;
        string pwConfirm = signUpInputPWConfirm.text;

        // 비밀번호와 비밀번호 확인이 일치하는지 확인
        if (pw != pwConfirm)
        {
            Debug.LogError("비밀번호와 비밀번호 확인이 일치하지 않습니다.");
            return;
        }

        // 회원가입 로직을 수행
        // 여기에 회원가입 관련 로직 추가 필요 @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        Debug.LogFormat("회원가입 정보: id={0}, nickname={1}, pw={2}", id, nickname, pw);

        // 회원가입 성공 후 로그인 창을 보여줌
        ShowLoginPanel();
    }

    // 로그인 패널을 활성화하고 회원가입 패널을 비활성화하는 함수
    public void ShowLoginPanel()
    {
        loginPanel.SetActive(true);
        signUpPanel.SetActive(false);
    }

    // 회원가입 패널을 활성화하고 로그인 패널을 비활성화하는 함수
    public void ShowSignupPanel()
    {
        signUpPanel.SetActive(true);
        loginPanel.SetActive(false);
    }
}
