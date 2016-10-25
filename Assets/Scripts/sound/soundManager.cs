using UnityEngine;
using System;
using System.Collections;


// 音管理クラス
public class SoundManager : MonoBehaviour {

	protected static SoundManager instance;

	public static SoundManager Instance
	{
		get
		{
			if(instance == null)
			{
				instance = (SoundManager) FindObjectOfType(typeof(SoundManager));

				if (instance == null)
				{
					Debug.LogError("SoundManager Instance Error");
				}
			}

			return instance;
		}
	}

	// 音量
	public SoundVolume volume = new SoundVolume();

	// === AudioSource ===
	// BGM
	private AudioSource BGMsource;
	// SE
	private AudioSource[] SEsources = new AudioSource[16];
	// 音声
	private AudioSource[] MESources = new AudioSource[16];

	// === AudioClip ===
	// BGM
	public AudioClip[] BGM;
	// SE
	public AudioClip[] SE;
	// 音声
	public AudioClip[] ME;

	void Awake (){
		GameObject[] obj = GameObject.FindGameObjectsWithTag("SoundManager");
		if( obj.Length > 1 ){
			// 既に存在しているなら削除
			Destroy(gameObject);
		}else{
			// 音管理はシーン遷移では破棄させない
			DontDestroyOnLoad(gameObject);
		}

		// 全てのAudioSourceコンポーネントを追加する

		// BGM AudioSource
		BGMsource = gameObject.AddComponent<AudioSource>();
		// BGMはループを有効にする
		BGMsource.loop = true;

		// SE AudioSource
		for(int i = 0 ; i < SEsources.Length ; i++ ){
			SEsources[i] = gameObject.AddComponent<AudioSource>();
		}

		// 音声 AudioSource
		for(int i = 0 ; i < MESources.Length ; i++ ){
			MESources[i] = gameObject.AddComponent<AudioSource>();
		}
	}

	void Update () {
		// ミュート設定
		BGMsource.mute = volume.Mute;
		foreach(AudioSource source in SEsources ){
			source.mute = volume.Mute;
		}
		foreach(AudioSource source in MESources ){
			source.mute = volume.Mute;
		}

		// ボリューム設定
		BGMsource.volume = volume.BGM;
		foreach(AudioSource source in SEsources ){
			source.volume = volume.SE;
		}
		foreach(AudioSource source in MESources ){
			source.volume = volume.ME;
		}
	}



	// ***** BGM再生 *****
	// BGM再生
	public void PlayBGM(int index){
		if( 0 > index || BGM.Length <= index ){
			return;
		}
		// 同じBGMの場合は何もしない
		if( BGMsource.clip == BGM[index] ){
			return;
		}
		BGMsource.Stop();
		BGMsource.clip = BGM[index];
		BGMsource.Play();
	}

	// BGM停止
	public void StopBGM(){
		BGMsource.Stop();
		BGMsource.clip = null;
	}


	// ***** SE再生 *****
	// SE再生
	public void PlaySE(int index){
		if( 0 > index || SE.Length <= index ){
			return;
		}

		// 再生中で無いAudioSouceで鳴らす
		foreach(AudioSource source in SEsources){
			if( false == source.isPlaying ){
				source.clip = SE[index];
				source.Play();
				return;
			}
		}  
	}

	// SE停止
	public void StopSE(){
		// 全てのSE用のAudioSouceを停止する
		foreach(AudioSource source in SEsources){
			source.Stop();
			source.clip = null;
		}  
	}


	// ***** 音声再生 *****
	// 音声再生
	public void PlayME(int index){
		if( 0 > index || ME.Length <= index ){
			return;
		}
		// 再生中で無いAudioSouceで鳴らす
		foreach(AudioSource source in MESources){
			if( false == source.isPlaying ){
				source.clip = ME[index];
				source.Play();
				return;
			}
		} 
	}

	// 音声停止
	public void StopME(){
		// 全ての音声用のAudioSouceを停止する
		foreach(AudioSource source in MESources){
			source.Stop();
			source.clip = null;
		}  
	}
}
// 音量クラス
[Serializable]
public class SoundVolume{
	public float BGM = 1.0f;
	public float ME = 1.0f;
	public float SE = 1.0f;
	public bool Mute = false;

	public void Init(){
		BGM = 1.0f;
		ME = 1.0f;
		SE = 1.0f;
		Mute = false;
	}
}