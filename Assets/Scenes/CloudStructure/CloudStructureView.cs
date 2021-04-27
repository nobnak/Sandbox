using CloudStructures;
using CloudStructures.Converters;
using CloudStructures.Structures;
using StackExchange.Redis;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CloudStructureView : MonoBehaviour {

    public const string KEY_TEST = "k_data";

    [SerializeField]
    protected string server = "127.0.0.1";

    protected Rect windowSize = new Rect(10, 10, 300, 300);

    protected RedisConnection redis;

    protected RedisString<Data> redisData;

    #region unity
    private void OnEnable() {
        redis = new RedisConnection(new RedisConfig(name, server), new SystemTextJsonConverter());
        redisData = new RedisString<Data>(redis, KEY_TEST, null);
    }
    private void OnDisable() {
    }
    private void OnGUI() {
        windowSize = GUILayout.Window(GetInstanceID(), windowSize, OnWindow, name);
    }
    private void Update() {
    }
    #endregion

    #region member
    protected void OnWindow(int id) {
        var conn = redis.GetConnection();
        GUI.enabled = conn != null && conn.IsConnected;

        using (new GUILayout.VerticalScope()) {
            if (GUILayout.Button("Set")) {
                var data = new Data() {
                    id = Time.frameCount,
                    position_x = Mathf.FloorToInt(10f * Random.value),
                    position_y = Mathf.FloorToInt(10f * Random.value),
                    birthTime = System.DateTimeOffset.Now.Ticks
                };
                Debug.Log($"Set. {data}");
                redisData.SetAsync(data).Wait();
            }
            if (GUILayout.Button("Get")) {
                redisData.GetAsync().ContinueWith(t => {
                    if (!t.IsFaulted) {
                        var data = t.Result;
                        Debug.Log($"Get. {data}");
                    }
                }).Wait();
            }
        }

        GUI.DragWindow();
    }
    #endregion

    #region definition
    public class Data {
        public int id;
        public float position_x;
        public float position_y;
        public long birthTime;

        #region interafce

        #region object
        public override string ToString() {
            var tmp = new StringBuilder();
            tmp.Append($"<{GetType().Name} : ");
            tmp.Append($"id={id}");
            tmp.Append($", position=({position_x},{position_y})");
            tmp.Append($", birth_time={birthTime}>");
            return tmp.ToString();
        }
        #endregion

        #endregion
    }
    #endregion
}
