#region

using Data.AssetsAddressables;
using Services.Factories.AbstractFactory;
using Services.Input;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Unit.Player
{
    public class PlayerController
    {
        public PlayerController(IAbstractFactory abstractFactory,
            Vector2 spawnPositionPlayer,
            float jumpForce,
            float moveSpeed,
            InputActionsReader inputActionsReader,
            Transform playerTargetTransform)
        {

            _spawnPositionPlayer = spawnPositionPlayer;
            _playerTargetTransform = playerTargetTransform;
            _abstractFactory = abstractFactory;
            _jumpForce = jumpForce;
            _speed = moveSpeed;

            inputActionsReader.OnMovementInput = Move;

            CreatePlayer();
        }

        private readonly IAbstractFactory _abstractFactory;
        private readonly Vector2 _spawnPositionPlayer;
        private readonly Transform _playerTargetTransform;
        private readonly float _jumpForce;
        private readonly float _speed;

        private Rigidbody2D _rigidbody2D;

        public void Jump()
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, (ForceMode2D)ForceMode.Impulse);
        }

        public bool IsNotJumping()
        {
            return _rigidbody2D.velocity.y <= 0.1f;
        }

        private void Move(float direction)
        {
            _rigidbody2D.velocity = new Vector2(direction * _speed, _rigidbody2D.velocity.y);
        }

        private async void CreatePlayer()
        {
            var playerInstance = await _abstractFactory.CreateInstance<GameObject>(AssetsAddressablesContainers.PLAYER);
            playerInstance.transform.position = _spawnPositionPlayer;

            _playerTargetTransform.SetParent(playerInstance.transform);
            _playerTargetTransform.localPosition = Vector3.zero;

            _rigidbody2D = playerInstance.GetComponent<Rigidbody2D>();
        }
    }
}