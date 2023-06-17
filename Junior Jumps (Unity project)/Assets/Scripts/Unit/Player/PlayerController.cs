#region

using Data.AssetsAddressables;
using Data.Setting;
using Services.Factories.AbstractFactory;
using Services.Input;
using UnityEngine;
using Zenject;

#endregion

namespace Unit.Player
{
    public class PlayerController
    {
        [Inject]
        public PlayerController(
            IAbstractFactory abstractFactory,
            PlayerSetting playerSetting,
            InputActionsReader inputActionsReader)
        {
            _jumpForce = playerSetting.NormalJumpForce;
            _moveSpeed = playerSetting.MoveSpeed;
            _abstractFactory = abstractFactory;

            inputActionsReader.OnMovementInput = Move;
        }

        private readonly IAbstractFactory _abstractFactory;
        private readonly float _jumpForce;
        private readonly float _moveSpeed;

        private Transform _playerTargetTransform;
        private Rigidbody2D _rigidbody2D;

        public void Jump()
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, (ForceMode2D)ForceMode.Impulse);
        }

        public bool IsNotJumping()
        {
            return _rigidbody2D.velocity.y <= 0.1f;
        }

        private void Move(float direction)
        {
            _rigidbody2D.velocity = new Vector2(direction * _moveSpeed, _rigidbody2D.velocity.y);
        }

        public async void CreatePlayer(Transform playerTargetTransform, Vector3 spawnPositionPlayer)
        {
            _playerTargetTransform = playerTargetTransform;

            var playerInstance = await _abstractFactory.CreateInstance<GameObject>(AssetsAddressablesContainers.PLAYER);
            playerInstance.transform.position = spawnPositionPlayer;

            _playerTargetTransform.SetParent(playerInstance.transform);
            _playerTargetTransform.localPosition = Vector3.zero;

            _rigidbody2D = playerInstance.GetComponent<Rigidbody2D>();
        }
    }
}